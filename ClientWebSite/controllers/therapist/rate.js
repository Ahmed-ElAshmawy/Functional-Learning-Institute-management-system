angular.module("module").controller("rateTherapist", ["$scope", "getAjax", "$http", "$location", "$routeParams", "serverUrl",
    function($scope, getAjax, $http, $location, $routeParams, serverUrl) {
        console.log("enter rate therpaist controll");
        $scope.isLoading = true;
        $scope.therapistId = $routeParams.id;
        $scope.therapistName = $routeParams.name;
        console.log("Name", $scope.therapistId, "Id", $scope.therapistName);
        $('#createRate').hide();
        getAjax.response(serverUrl + "api/Rates?therapistId=" + $scope.therapistId)
            .then((response) => {
                $scope.rateList = response.data;
                $scope.isLoading = false;
            }, (error) => {})


        $scope.redirectToRate = () => {
            $location.path("/addRate/" + $scope.therapistId + "/" + $scope.therapistName);
        }

        $scope.addRate = () => {
            console.log("add event");
            $('#createRate').show();
        }






        $scope.deleteModal = (idRate, rate) => {
            console.log("enter delet rate modal");
            $scope.id = idRate;
            $scope.rate = rate;
            $('#therapistModal').modal('show');
        }


        $scope.deleteRate = () => {
            console.log("delete rate id", $scope.id, "rate value", $scope.rate);
            var url = serverUrl + "api/Rates/" + $scope.id + "?therapistId=" + $scope.therapistId;
            var config = 'contenttype';
            $http.delete(url, config).then(function(response) {
                console.log(response);
                if (response.status == 200) {
                    getAjax.response(serverUrl + "api/Rates?therapistId=" + $scope.therapistId)
                        .then((response) => {
                            console.log("enter second get ajax", $scope.therapistId);
                            $scope.rateList = response.data;
                            console.log("getdata", $scope.rateList);
                            $scope.isLoading = false;
                            $('#therapistModal').modal('hide');
                            $('.in').hide();
                        }, (error) => {})
                }
            }, function(error) {
                console.log(error);
            });
        }



        $scope.backToTherapist = function() {
            console.log("Enter click event to go back")
            $location.path("/therapist");
        }

    }
])