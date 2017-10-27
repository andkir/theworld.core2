(function () {

    angular.module('simpleControls', [])
        .directive('waitCursor', waitCursor);

	function waitCursor() {
	    return {
	    	templateUrl: '/view/waitCursor.html',
	    	restrict: 'E',
			scope: {
                show:'=displayWhen'
			}
	    }
	}

})();