
LiveOdiaApp.directive('imageShowDirective', function () {
    return {
        restrict: 'E',
        //scope: {
        //    imagePosition:'@'
        //},
        transclude: 'true',
        templateUrl: function (elem, attr) {
            
            var imgPosition = attr.imageposition;
            if (imgPosition === 'top-left')
                return 'App/Directives/image-top-left.html';
            else if (imgPosition === 'top-right')
                return 'App/Directives/image-top-right.html';
            else
                return 'App/Directives/error.html';
        }
        //link: function (scope, ele, attr) {
        //    
        //    scope.imagePosition;

        //}
    };
});