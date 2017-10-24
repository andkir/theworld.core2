(function () {

    angular.module("app-trips").controller("tripsController", tripsController);

    function tripsController() {
        var vm = this;

        vm.trips = [{
            name: 'USA Trip',
            created: new Date()
        },
            {
                name: 'Europe Trip',
                created: new Date()
            }];

        vm.newTrip = {};

        vm.addTrip = function () {
            vm.trips.push(vm.newTrip);
            vm.newTrip = {};
        }
    }

})();