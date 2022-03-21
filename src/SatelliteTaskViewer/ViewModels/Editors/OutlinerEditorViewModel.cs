using System;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.ViewModels.Containers;
using SatelliteTaskViewer.ViewModels.Entities;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;

namespace SatelliteTaskViewer.ViewModels.Editors
{
    public enum DisplayMode { Visual, Logical }

    public class OutlinerEditorViewModel : ViewModelBase
    {
        private readonly ScenarioContainerViewModel _scenario;

        public OutlinerEditorViewModel(ScenarioContainerViewModel scenario)
        {
            _scenario = scenario;

            this.WhenAnyValue(s => s.SelectedMode).Subscribe(mode =>
            {
                switch (mode)
                {
                    case DisplayMode.Visual:
                        InvalidateVisual(Entities);
                        break;
                    case DisplayMode.Logical:
                        InvalidateLogical(FrameRoot);
                        break;
                    default:
                        break;
                }
            });

            this.WhenAnyValue(s => s.SelectedItem).Subscribe(item =>
            {
                switch (SelectedMode)
                {
                    case DisplayMode.Visual:
                        if (item is BaseEntity entity)
                        {
                            CurrentEntity = entity;
                        }
                        break;
                    case DisplayMode.Logical:
                        if (item is FrameViewModel frame)
                        {
                            CurrentFrame = frame;
                        }
                        break;
                    default:
                        break;
                }
            });

            this.WhenAnyValue(s => s.FrameRoot).Subscribe(frameRoot => InvalidateLogical(frameRoot));

            this.WhenAnyValue(s => s.Entities).Subscribe(entities => InvalidateVisual(entities));

            SelectedMode = DisplayModes.FirstOrDefault();
        }

        private void InvalidateVisual(ImmutableArray<BaseEntity> entities)
        {
            if (entities != null)
            {
                Items = new ObservableCollection<ViewModelBase>(entities);
                SelectedItem = entities.FirstOrDefault();
            }
        }

        private void InvalidateLogical(ImmutableArray<FrameViewModel> frameRoot)
        {
            if (frameRoot != null)
            {
                Items = new ObservableCollection<ViewModelBase>(frameRoot);
                SelectedItem = frameRoot.FirstOrDefault();
            }
        }

        [Reactive]
        public ImmutableArray<FrameViewModel> FrameRoot { get; set; }

        [Reactive]
        public FrameViewModel? CurrentFrame { get; set; }

        [Reactive]
        public ImmutableArray<BaseEntity> Entities { get; set; }

        [Reactive]
        public BaseEntity? CurrentEntity { get; set; }

        [Reactive]
        public DisplayMode SelectedMode { get; set; }

        public List<DisplayMode> DisplayModes => Enum.GetValues(typeof(DisplayMode)).Cast<DisplayMode>().ToList();

        [Reactive]
        public ObservableCollection<ViewModelBase> Items { get; set; }

        [Reactive]
        public ViewModelBase? SelectedItem { get; set; }

        public void OnSetCameraTo(ViewModelBase item)
        {
            if (item is ITargetable target)
            {
                _scenario.SetCameraTo(target);

                SelectedItem = item;
            }
        }
    }
}
