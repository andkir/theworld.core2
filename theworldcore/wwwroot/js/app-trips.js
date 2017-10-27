(function() {

    angular.module("app-trips", ["simpleControls", "ngRoute"]).config(function ($routeProvider) {

        $routeProvider.when("/",
            {
                controller: "tripsController",
                controllerAs: "vm",
                templateUrl: "/view/tripsView.html"
            });

        $routeProvider.when("/editor/:tripName",
            {
                controller: "tripsEditorController",
                controllerAs: "vm",
                templateUrl: "/view/tripsEditorView.html"
            });


        $routeProvider.otherwise({ redirectTo: "/" });
    });

})();

