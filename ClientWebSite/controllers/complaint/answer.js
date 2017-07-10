angular.module("module").controller("answercomplaint", ["$scope", "getAjax", "$http", "$location", "$routeParams", "auth", "serverUrl",
    function($scope, getAjax, $http, $location, $routeParams, auth, serverUrl) {
        var complaintId = $routeParams.Id;
        console.log("ComplaintID", complaintId)
        $scope.sendData = function() {
            console.log($scope.body);
            $http.put(serverUrl + "api/Complaints/PutComplaint?id=" + complaintId + "&answer=" + $scope.body).
            then((data) => {
                    console.log(data);
                    $location.path("/Allcomplaint");
                },
                (error) => {
                    console.log(error);
                    console.log("errrror")
                });
        }
        $scope.back = () => {
            $location.path("/Allcomplaint");
        }
        $scope.reset = () => {
            $scope.body = $scope.title = "";
            $scope.error = "";
        }




    }
])