
LiveOdiaApp.directive('imageDirective', ['$document', '$rootScope', function ($document, $rootScope) {
    return {
        link: function (scope, element, attr) {
            var startX = 0, startY = 0, x = 0, y = 0;

            element.css({
                position: 'absolute',
                border: '1px solid red',
                backgroundColor: 'lightgrey',
                cursor: 'pointer'
            });

            element.on('mousedown', function (event) {
                // Prevent default dragging of selected content
                event.preventDefault();
                startX = event.pageX - x;
                startY = event.pageY - y;
                console.log(startX + "-" + startY);
                var startXY = [startX, startY];
                $rootScope.startXY = startXY;
                console.log($rootScope.startXY);
                $document.on('mousemove', mousemove);
                $document.on('mouseup', mouseup);
            });

            function mousemove(event) {
                y = event.pageY - startY;
                x = event.pageX - startX;
                console.log(event.pageY, event.pageX);
                console.log(x, y);
                element.css({
                    top: y + 'px',
                    left: x + 'px'
                });
            }

            function mouseup() {
                $document.off('mousemove', mousemove);
                $document.off('mouseup', mouseup);
            }
        }
    };
}]);