﻿using Avalonia.Controls;
using Avalonia.Xaml.Interactivity;

namespace SatelliteTaskViewer.Avalonia.Behaviors
{
    public class AttachEditorBehavior : Behavior<Control>
    {
        private AttachEditor _input;

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject != null)
            {
                _input = new AttachEditor(AssociatedObject);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (AssociatedObject != null)
            {
                _input?.Detach();
            }
        }
    }
}
