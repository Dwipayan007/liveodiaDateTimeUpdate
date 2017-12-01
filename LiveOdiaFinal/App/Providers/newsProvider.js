/// <reference path="../app.js" />
LiveOdiaApp.provider("news", function () {
    var $http;
    var newsData = "";
    this.getAllNewsValue = function (newvalue) {
        newsData = newvalue;
        $http.get('https://api.github.com/users/mralexgray/repos').success(function (data) {
            newsData = data;
        }).error(function (err, status) {
            
        });
    };
    this.$get = function () {
        return newsData;
    };
});
//LiveOdiaApp.config(function (newsProvider) {
    
//})