<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/310298166/20.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1125301)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# How to load Scheduler Control data on demand

This example illustrates how to load data in the scheduler only for the selected date interval. The example consists of two solutions. Each solution has its own SchedulingViewModel:

* _CommonDbContext_. This example uses a single `DbContext` for the application.
* _DbContextPerRequest_. This example follows the One `DbContext` per request approach.
* _Shared_. The project contains the data and the view, which are the same for both solutions. The events are mapped to the view model’s handlers.

## Files to Review

- [SchedulingViewModel.cs](./CS/CommonDbContext/ViewModels/SchedulingViewModel.cs) ([SchedulingViewModel.vb](./VB/CommonDbContext/ViewModels/SchedulingViewModel.vb))
- [SchedulingViewModel.cs](./CS/DbContextPerRequest/ViewModels/SchedulingViewModel.cs) ([SchedulingViewModel.vb](./VB/DbContextPerRequest/ViewModels/SchedulingViewModel.vb))
- [SchedulingView.xaml](./CS/Shared/Views/SchedulingView.xaml) ([SchedulingView.xaml](./VB/Shared/Views/SchedulingView.xaml))
- [SchedulingView.xaml.cs](./CS/Shared/Views/SchedulingView.xaml.cs) ([SchedulingView.xaml.vb](./VB/Shared/Views/SchedulingView.xaml.vb))

## Documentation

* [Load Data on Demand](https://docs.devexpress.com/WPF/402187/controls-and-libraries/scheduler/data-binding/load-data-on-demand)



