LiveOdiaApp.controller('adminController', ['$scope', '$rootScope', '$filter', '$location', '$window', 'adminServiceFactory','homeServiceFactory', function ($scope, $rootScope, $filter, $location, $window,adminServiceFactory,homeServiceFactory) {
    $window.ga('send', 'adminview', $location.path());
    $scope.hotnews = {};
    $scope.Newstory = {};
    $scope.Topnews = {};
    $scope.hnewsTitle = {};
 	$scope.relatednews = "";
	$scope.rnewsdata = "";
	$scope.related = [];
    $scope.impnews = {};
    $scope.selectedOption;
    $scope.ntitle = "";
    $scope.mesgHnews = "";
    $scope.mesgTnews = "";
    $scope.mesgNnews = "";
    $scope.mesgInews = "";
    $scope.impnews.impdate = "";
    $scope.hotnews.nstorydt = "";
    $scope.Newstory.hotnewsdt = "";
    $scope.Topnews.topnewsdt = "";
    $scope.impnewsDate = new Date();
    $scope.hotnewsDate = new Date();
    $scope.newstoryDate = new Date();
    $scope.topnewsDate = new Date();
    $scope.myDate = new Date();
	$scope.myColor1 = "#000000";

    $scope.$watch('myColor1', function (val) {
        $scope.myColor1 = val;
    });

    $scope.myColor2 = "#000000";

    $scope.$watch('myColor2', function (val) {
        $scope.myColor2 = val;
    });

    $scope.myColor3 = "#000000";

    $scope.$watch('myColor3', function (val) {
        $scope.myColor3 = val;
    });

    $scope.myColor4 = "#000000";

    $scope.$watch('myColor4', function (val) {
        $scope.myColor4 = val;
    });
 	$scope.selected = undefined;
    $scope.countries = [
     { ititle: 'Afghanistan', code: 'AF' },
     { ititle: 'Antigua and Barbuda', code: 'AG' },
     { ititle: 'Bahamas', code: 'BS' },
     { ititle: 'Cambodia', code: 'KH' },
     { ititle: 'Cape Verde', code: 'CV' }
    ];
    $scope.getAllData = function () {
        debugger;
        $scope.allData = [];
        homeServiceFactory.getImpNews().then(function (data) {
            debugger;
            $scope.countries = data;
        });
    };

 $scope.tab = 1;

    $scope.selectTab = function (setTab) {
        $scope.tab = setTab;
    };
    $scope.isSelected = function (checkTab) {
        return $scope.tab === checkTab;
    };

    $scope.loader = {
        loading1: false,
        loading2: false,
        loading3: false,
        loading4: false
    };



    $scope.minDate = new Date(
        $scope.myDate.getFullYear(),
        $scope.myDate.getMonth() - 2,
        $scope.myDate.getDate());

    $scope.maxDate = new Date(
        $scope.myDate.getFullYear(),
        $scope.myDate.getMonth() + 2,
        $scope.myDate.getDate());

    $scope.onlyWeekendsPredicate = function (date) {
        var day = date.getDay();
        return day === 0 || day === 6;
    };
 	$scope.getRelated = function () {
        adminServiceFactory.getRelated().then(function (rdata) {
            // $scope.related[0] = { rid: "0", rnews: "Please Select" };
            $scope.related = rdata;
            $scope.related.splice(0, 0, { rid: "0", rnews: "Please Select" });
            $scope.relatednews = $scope.related[0];
        });
    };
    $scope.changeRelated = function () {
        debugger;
        $scope.rnewsdata = $scope.relatednews.rid;
    };
    $scope.AddNewRelated = function (rdata) {
        adminServiceFactory.AddNewRelated(rdata).then(function (res) {
            if (res)
                $scope.getRelated();
        });
    };
    $scope.updateDate = function () {
        debugger;
        $scope.SelectedDate = $filter('date')(new Date($scope.myDate), 'dd-MM-yyyy');
        adminServiceFactory.updateNewsDate($scope.SelectedDate).then(function (res) {
            if (res) {
                //$location.path('/admin');
                $location.path('/home');
            }
        });

    };
    $scope.DeleteAllNews = function () {
        adminServiceFactory.DeleteAllNews().then(function (res) {
            if (res) {
                $scope.mesg = "You have deleted all News Please add some fresh news...";
            }
        });
    }
    $scope.changeOption = function () {

        $scope.ntitle = $scope.selectedOption.ndid;
    };

    $scope.AddNewCategory = function (cname) {
        adminServiceFactory.AddCategory(cname).then(function (data) {

        });
    };

    $scope.DownloadNews = function () {
        $scope.SelectedDate = $filter('date')(new Date($scope.myDate), 'dd-MM-yyyy');
        adminServiceFactory.DownloadNews($scope.SelectedDate).then(function (pdfname) {
            var blob = new Blob(([pdfname]), { type: "application/pdf" });
            var fileURL = URL.createObjectURL(blob);
            var a = document.createElement('a');
            a.href = fileURL;
            a.target = '_blank';
            a.download = "mynews" + '.pdf';
            document.body.appendChild(a);
            a.click();
            //saveAs(blob, "download.pdf");
        });
    };

    $scope.submitImpNews = function () {
        debugger;
 		if ($scope.impnews.title != undefined) {
            if ($scope.impnews.title.length != 0) {
        $scope.impnews.impdate = $filter('date')(new Date($scope.impnewsDate), 'dd-MM-yyyy');
        $scope.loader.loading4 = true;
        var file = {};
        file = $scope.impnews;
 		file['myColor'] = $scope.myColor1;
 		file["relateNews"] = $scope.relatednews.rid;
        file["ImpNews"] = "Inews";
        if ($scope.myFile4 !== undefined)
            file["file"] = $scope.myFile4;
        adminServiceFactory.uploadFileToUrl(file).then(function (data) {
            if (data === "") {
                $scope.loader.loading4 = false;
                $scope.mesgInews = "Uploaded Successfully";
            }
            else {
                $scope.loader.loading4 = false;
                $scope.mesgInews = "Not Successful";
            }
 			file['myColor'] = "#000000";
            file = null;
            $scope.impnews = {};
            $scope.resetForm("submitImpNews");
        });
		}
		else {
                $scope.mesgInews = 'Please Enter the title';
            }
        }
        else {
            $scope.mesgInews = 'Please Enter the title';
        }
    };

    $scope.submitHotNews = function () {
		if ($scope.hotnews.title != undefined) {
			if ($scope.hotnews.title.length != 0) {
			$scope.hotnews.hotnewsdt = $filter('date')(new Date($scope.hotnewsDate), 'dd-MM-yyyy');
			$scope.loader.loading1 = true;
			var file = {};
			file = $scope.hotnews;
			file["HotNews"] = "hnews";
			file["selOption"] = $scope.selectedOption.ndid;
			file['myColor'] = $scope.myColor2;
			file["relateNews"] = $scope.relatednews.rid;
			if ($scope.myFile1 !== undefined)
				file["file"] = $scope.myFile1;
			adminServiceFactory.uploadFileToUrl(file).then(function (data) {
				if (data === "") {
					$scope.loader.loading1 = false;
					$scope.mesgHnews = "Uploaded Successfully";
				}
				else {
					$scope.loader.loading1 = false;
					$scope.mesgHnews = "Not Successful";
				}
				file['myColor'] = "#000000";
				file = null;
				$scope.hotnews = {};
				$scope.resetForm("submitHotNews");
			});
		}
		else {
                $scope.mesgHnews = 'Please Enter the title';
            }
        }
        else {
            $scope.mesgHnews = 'Please Enter the title';
        }
    };

    $scope.submitNewstory = function () {
		if ($scope.Newstory.ntitle != undefined) {
			if ($scope.Newstory.ntitle.length != 0) {
        $scope.Newstory.nstorydt = $filter('date')(new Date($scope.newstoryDate), 'dd-MM-yyyy');
        $scope.loader.loading2 = true;
        var file = {};
        file = $scope.Newstory;
        file["Newstory"] = "nstory";
		file["relateNews"] = $scope.relatednews.rid;
 		file['myColor'] = $scope.myColor3;
        if ($scope.myFile2 !== undefined)
            file["file"] = $scope.myFile2;
        adminServiceFactory.uploadFileToUrl(file).then(function (data) {

            if (data === "") {
                $scope.loader.loading2 = false;
                $scope.mesgNnews = "Uploaded Successfully";
            }
            else {
                $scope.loader.loading2 = false;
                $scope.mesgNnews = "Not Successful";
            }
 			$scope.myColor3 = "#000000";
            file = null;
            $scope.Newstory = {};
            $scope.resetForm('submitNewstory');
        });
		}
		else {
                $scope.mesgNnews = 'Please Enter the title';
            }
        }
        else {
            $scope.mesgNnews = 'Please Enter the title';
        }
    };

    $scope.getAllHotNewsTitle = function () {

        adminServiceFactory.getHotFullNewsTitle().then(function (hnewsdata) {
            if (hnewsdata) {

                $scope.hnewsTitle = hnewsdata;
                $scope.selectedOption = $scope.hnewsTitle[0];
            }
        });
    };

    $scope.submitTopNews = function () {
		if ($scope.Topnews.ttitle != undefined) {
			if ($scope.Topnews.ttitle.length != 0) {
        $scope.Topnews.topnewsdt = $filter('date')(new Date($scope.topnewsDate), 'dd-MM-yyyy');
        $scope.loader.loading3 = true;
        var file = {};
        file = $scope.Topnews;
        file["TopNews"] = "tnews";
        file["relateNews"] = $scope.relatednews.rid;
  		file['myColor'] = $scope.myColor4;
        if ($scope.myFile3 !== undefined)
            file["file"] = $scope.myFile3;
        adminServiceFactory.uploadFileToUrl(file).then(function (data) {

            if (data === "") {
                $scope.loader.loading3 = false;
                $scope.mesgTnews = "Uploaded Successfully";
            }
            else {
                $scope.loader.loading3 = false;
                $scope.mesgTnews = "Not Successful";
            }
 			$scope.myColor4 = "#000000";
            file = null;
            $scope.Topnews = {};
            $scope.resetForm("submitTopNews");
        });
		}
		else {
                $scope.mesgTnews = 'Please Enter the title';
            }
        }
        else {
            $scope.mesgTnews = 'Please Enter the title';
        }
    };
    $scope.getAllHotNewsTitle();

    $scope.resetForm = function (filereset) {
        if (filereset === "submitNewstory") {
            angular.element(document.querySelector('#file2')).val(null);
            $scope.myFile2 = undefined;
        }
        else if (filereset === "submitTopNews") {
            angular.element(document.querySelector('#file3')).val(null);
            $scope.myFile3 = undefined;
        }
        else if (filereset === "submitHotNews") {
            angular.element(document.querySelector('#file1')).val(null);
            $scope.myFile1 = undefined;
        }
        else if (filereset === "submitImpNews") {
            angular.element(document.querySelector('#file4')).val(null);
            $scope.myFile4 = undefined;
        }
    };
	$scope.getPrioritynews = function () {
        adminServiceFactory.getPrioritynews().then(function (pnews) {
            $scope.pnews = pnews;
        });
    };
    $scope.ReorderPriority = function () {
        $scope.pnews;
       $scope.mesg = '';
        var priority = 0;
        angular.forEach($scope.pnews, function (value, key) {
            if (parseInt(value.priority) > 6)
                priority = parseInt(value.priority);
        });
        if (priority <= 6) {
            adminServiceFactory.ReorderPriority($scope.pnews).then(function (res) {

            });
        }
        else
            $scope.mesg = 'Please Enter the priority that should be less than 6';
    };
    $scope.getAllData();
    $scope.getRelated();
    $scope.getPrioritynews();

}]);
