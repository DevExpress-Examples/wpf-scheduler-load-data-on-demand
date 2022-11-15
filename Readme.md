<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/310298166/20.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1125301)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# How to load Scheduler Control data on demand

This Scheduler control example only loads appointments that fall into the specified date interval. The solution consists of three parts.

* _Shared_. This shared project contains the data and the view used by other projects. The view maps events to view model’s handlers.
* _CommonDbContext_. This project implements a `SchedulingViewModel` that uses a single `DbContext` for the application.
* _DbContextPerRequest_. This project implements a `SchedulingViewModel` that creates a new `DbContext` per each request.

## Files to Review

_Shared_ project:

- [SchedulingView.xaml](./CS/Shared/Views/SchedulingView.xaml) ([SchedulingView.xaml](./VB/Shared/Views/SchedulingView.xaml))
- [SchedulingView.xaml.cs](./CS/Shared/Views/SchedulingView.xaml.cs) ([SchedulingView.xaml.vb](./VB/Shared/Views/SchedulingView.xaml.vb))

_CommonDbContext_ project:

- [SchedulingViewModel.cs](./CS/CommonDbContext/ViewModels/SchedulingViewModel.cs) ([SchedulingViewModel.vb](./VB/CommonDbContext/ViewModels/SchedulingViewModel.vb))

_DbContextPerRequest_ project: 

- [SchedulingViewModel.cs](./CS/DbContextPerRequest/ViewModels/SchedulingViewModel.cs) ([SchedulingViewModel.vb](./VB/DbContextPerRequest/ViewModels/SchedulingViewModel.vb))

## Documentation

* [Load Data on Demand](https://docs.devexpress.com/WPF/402187/controls-and-libraries/scheduler/data-binding/load-data-on-demand)



