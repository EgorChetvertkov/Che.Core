# Result pattern

This is an approach in programming that allows a method to return not only the value it is supposed to produce, but also additional information about the result of the operation. This template is especially useful in situations where a method may fail and you need to pass error information.

Instead of returning only the data type or throwing an exception in case of an error, the method returns an object of the special Result class, which contains information about whether the operation was successful, as well as the result itself or error information.

# Mail

Sending emails using the FluentEmail library. The ability to send both Razor templates and plain text is supported. For the module to work, it is necessary to implement the Outbox template.
