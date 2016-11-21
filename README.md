# SceneSkope.Extensions.Logging.RequestPathFilter
Asp.Net Core RequestPath logging filter

Simple Asp.Net core logging filter to allow a request path to be ignored. 
Very useful to ignore probe paths when running behind an application gateway or load balancer.

To use add the following line to Startup.cs

    var filteredFactory = loggerFactory.WithRequestPathFilter("/api/probe");

And use filteredFactory in the as the logger factory
