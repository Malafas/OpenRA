Architecture
============
## MVP: Model-View-Presenter

You can apply a MV* pattern to games, very easily in fact, and create a clean code base that's organized, maintainable and can be easily changed (we all know how volatile a game's design and feature set can be!). So lets see why an MVP pattern could be presented in OpenRa. 

MVP is a pattern developed from the Model/View/Controller Pattern (MVC), because the MVC pattern allows for viewing access to the Model component. This functionality requires the view to have logic as a component. The view also has more dependency with the user interface, that is, when the code is changed, it affects the user interface. This makes the system to be more complex, harder to test, and harder to maintain.

MVP Pattern is a model that separates control or logic from view, called the Presenter, making the view only for presentation and interface with user. The presenter is mainly responsible for receiving input data from the view to manage accessibility between the view and model. 
The relationship between the view and presenter is similar to the Decorator Pattern. The presenter is related to the view in the components, where one view can have more than one presenter. The MVP pattern has the presenter operates and sends results to view. The non-dependency between them allows the View and presenter to be less complex, and one presenter can be reused with different views.

In our case we can say that the MVP design pattern is preferred over MVC because this application needs to provide support for multiple user interface technologies. It is also preferred because it has complex user interface with a lot of user interaction. 
