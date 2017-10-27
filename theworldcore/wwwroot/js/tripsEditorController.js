(function () {

    angular.module("app-trips").controller("tripsEditorController", tripsEditorController);

    function tripsEditorController($routeParams, $http) {
        var vm = this;

        vm.isBusy = true;
        vm.name = "Andrii";
        vm.tripName = $routeParams.tripName;
        vm.errorMessage = "";
        vm.stops = [];

        $http.get('/api/trips/' + vm.tripName+"/stops" )
            .then(function (response) {
                vm.stops = angular.copy(response.data);
            })
            .catch(function (e) {
                vm.errorMessage = "Failed to load data:" + e.statusText;
            }).finally(function () {
                vm.isBusy = false;
            });
    }

})();