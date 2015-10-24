using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Oleg_ivo.Base.WPF.LogBinding
{
    public class EventViewBox : RichTextBox
    {
        static EventViewBox()
        {
            IsReadOnlyProperty.OverrideMetadata(typeof(EventViewBox), new FrameworkPropertyMetadata(true));
            IsReadOnlyCaretVisibleProperty.OverrideMetadata(typeof(EventViewBox), new FrameworkPropertyMetadata(true));
            IsUndoEnabledProperty.OverrideMetadata(typeof(EventViewBox), new FrameworkPropertyMetadata(false));
        }


        public EventViewBox()
        {
            this.KeepEventCount = 1000;
            //this.Document.PageWidth = 2000;
            this.Document.Blocks.Clear();
        }

        public static readonly DependencyProperty EventSourceProperty =
            DependencyProperty.Register(
                "EventSource", typeof(IObservable<object>), typeof(EventViewBox),
                new UIPropertyMetadata(null, EventSourceChanged)
                );

        [Bindable(true)]
        public IObservable<object> EventSource
        {
            get { return (IObservable<object>)GetValue(EventSourceProperty); }
            set { SetValue(EventSourceProperty, value); }
        }


        private static void EventSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var logViewBoxStrongRef = (EventViewBox) dependencyObject;
            var logViewBoxWeakRef = new WeakReference(logViewBoxStrongRef);

            var eventSource = ((IObservable<object>)e.NewValue);

            var logSubscription = eventSource == null ? null :
                eventSource
                .TakeWhile(_ => logViewBoxWeakRef.IsAlive)
                .ObserveOn(logViewBoxStrongRef.Dispatcher)
                .Subscribe(logEvent =>
                {
                    var logViewBox = (EventViewBox) logViewBoxWeakRef.Target;
                    if (logViewBox == null) return;

                    // TODO: DataTemplating
                    var block = new Paragraph(new Run(logEvent.ToString()));
                    ApplyStyleFor(logViewBox, block, logEvent);

                    logViewBox.BeginChange();
                    try
                    {
                        var document = logViewBox.Document;

                        while (document.Blocks.Count > logViewBox.KeepEventCount)
                            document.Blocks.Remove(document.Blocks.FirstBlock);

                        // TODO: Такая же логика как VS: если в конце то оставаться в конце
                        //bool isAtEnd = logViewBox.CaretPosition.CompareTo(logViewBox.CaretPosition.DocumentEnd) == 0;
                        document.Blocks.Add(block);
                        //if (isAtEnd)
                        {
                            logViewBox.CaretPosition = logViewBox.CaretPosition.DocumentEnd;
                            logViewBox.ScrollToEnd();
                        }
                    }
                    finally
                    {
                        logViewBox.EndChange();
                    }
                });

            logViewBoxStrongRef.ReplaceSubscription(logSubscription);
        }

        public static readonly DependencyProperty StyleSelectorProperty =
            DependencyProperty.Register("StyleSelector", typeof(StyleSelector), typeof(EventViewBox));

        [Bindable(true)]
        public StyleSelector StyleSelector
        {
            get { return (StyleSelector) GetValue(StyleSelectorProperty); }
            set { SetValue(StyleSelectorProperty, value); }
        }

        private static void ApplyStyleFor(EventViewBox eventViewBox, Paragraph block, object logEvent)
        {
            var styleSelector = eventViewBox.StyleSelector;
            if (styleSelector != null)
            {
                var styleBlock = styleSelector.SelectStyle(logEvent, block);
                if (styleBlock != null)
                {
                    block.Style = styleBlock;
                    if (!styleBlock.Setters.OfType<Setter>().Any(s => s.Property == Block.MarginProperty))
                    {
                        block.Margin = new Thickness(0);
                    }
                }
            }
        }

        private readonly SerialDisposable subscription = new SerialDisposable();

        private void ReplaceSubscription(IDisposable newSubscription)
        {
            subscription.Disposable = newSubscription;
        }

        

        [DefaultValue(1000)]
        public int KeepEventCount { get; set; }

    }
}
