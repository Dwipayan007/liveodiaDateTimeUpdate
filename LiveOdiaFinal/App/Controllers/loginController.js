
LiveOdiaApp.controller('loginController', ['$scope', '$rootScope', '$location', '$routeParams', '$window', 'loginServiceFactory', 'sharedService', function ($scope, $rootScope, $location, $routeParams, $window,loginServiceFactory, sharedService) {
    
    $scope.autho = false;
    //login data
    $scope.loginData = {
        userName: "",
        password: "",
        userRole: "1"
    };
    $scope.login = function () {
        $window.ga('send', 'event', 'Login Page', 'Login Tried');
        loginServiceFactory.login($scope.loginData).then(function (ldata) {
            if (ldata === false) {
                $scope.message = "Please Validate the Username and Password";
            }
            else {
                $location.path('/admin');
            }
        });
    };

    $(window, document, undefined).ready(function () {

        $('input').blur(function () {
            var $this = $(this);
            if ($this.val())
                $this.addClass('used');
            else
                $this.removeClass('used');
        });

        var $ripples = $('.ripples');

        $ripples.on('click.Ripples', function (e) {

            var $this = $(this);
            var $offset = $this.parent().offset();
            var $circle = $this.find('.ripplesCircle');

            var x = e.pageX - $offset.left;
            var y = e.pageY - $offset.top;

            $circle.css({
                top: y + 'px',
                left: x + 'px'
            });

            $this.addClass('is-active');

        });

        $ripples.on('animationend webkitAnimationEnd mozAnimationEnd oanimationend MSAnimationEnd', function (e) {
            $(this).removeClass('is-active');
        });

    });
}]);