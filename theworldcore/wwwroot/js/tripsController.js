(function () {

    angular.module("app-trips").controller("tripsController", tripsController);

    function tripsController($http) {
        var vm = this;
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newTrip = {};

        $http.get('/api/trips')
            .then(function (response) {
                vm.trips = angular.copy(response.data);
            })
            .catch(function (e) {
                vm.errorMessage = "Failed to load data:" + e.statusText;
            }).finally(function () {
                vm.isBusy = false;
            });




        vm.addTrip = function () {
            vm.errorMessage = "";
            vm.isBusy = true;

            $http.post('/api/trips', vm.newTrip)
                .then(function (response) {
                    vm.trips.push(vm.newTrip);
                })
                .catch(function (e) {
                    vm.errorMessage = "Failed to add new trip data:" + e.statusText;
                }).finally(function () {
                    vm.isBusy = false;
                    vm.newTrip = {};

                });

        }
    }

})();