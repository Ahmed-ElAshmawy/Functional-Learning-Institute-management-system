angular.module("module").controller("addRate", ["$scope", "getAjax", "$http", "$location", "$routeParams", "serverUrl",
    function($scope, getAjax, $http, $location, $routeParams, serverUrl) {
        console.log("enter add rate");
        $scope.therapistId = $routeParams.id;
        $scope.therapistName = $routeParams.name;

        $scope.createRate1 = () => {
            console.log("creat new rate");
            $scope.date = new Date();
            $scope.rateObj = {
                "TherapistId": $scope.therapistId,
                "Date": $scope.date,
                "BcValue": $scope.bcValue,
                "OtherValue": $scope.otherValue
            }
            console.log("object that send", $scope.rateObj);
            $http({
                method: "POST",
                url: serverUrl + "api/Rates",
                data: $scope.rateObj,
                header: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function(data) {
                console.log("success", data.data);
                $('#createRate').hide();
                $scope.rateList = data.data;
                getAjax.response(serverUrl + "api/Rates?therapistId=" + $scope.therapistId)
                    .then((response) => {
                        /* console.log("enter second get ajax", $scope.therapistId);
                         $scope.rateList = response.data;
                         console.log("getdata", $scope.rateList);
                         $scope.isLoading = false;*/
                        $location.path("/rateTherapist/" + $scope.therapistId + "/" + $scope.therapistName);
                    }, (error) => {})
                console.log("return success", $scope.rateList);

            }, function(error) {
                console.log("error", error);
            });
        }

    }
]);