
LiveOdiaApp.factory('sharedService', ['$rootScope', function ($rootScope) {
   
    var shareServiceFactory = {};
    //shareServiceFactory.aldata = '';
    //shareServiceFactory.idata = '';
   // shareServiceFactory.rdata = '';
    shareServiceFactory.hnid = '';

    //ste the Selected Config
    var _getHDetailNews = function (hnid) {
       
        this.hnid = hnid;
        this.broadcastHdetailNews();
    };

    //broadcast the run data
    var _broadcastHdetailNews = function () {
        return shareServiceFactory.hnid;
    };

    shareServiceFactory.broadcastHdetailNews = _broadcastHdetailNews;
    shareServiceFactory.getHotDetailNews = _getHDetailNews;

    return shareServiceFactory;
}]);