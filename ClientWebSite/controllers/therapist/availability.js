angular.module("module").controller("therapistAvailability", ["$scope", "getAjax", "$filter", "$http", "$location", "$routeParams", "auth",
    function($scope, getAjax, $filter, $http, $location, $routeParams, auth) {
        $scope.therapistId = auth.getUserId();
        $scope.isLoading = true;
        getAll = () => {
            getAjax.response("http://localhost:9408/api/TherapistAvailability/GetAvailabilityForTherapistById?id=" + $scope.therapistId).
            then(function(data) {
                console.log(data.data);
                $scope.show = data.data;
                $scope.isLoading = false;

            }, function(error) {
                console.log(error);
                if (error.status == "401")
                    $location.path("/unauthorized");
            });
        }
        getAll();
        $scope.cancelDate = (id, isBusy, slotId, date) => {
            $scope.id = id;
            $scope.isBusy = isBusy;
            $scope.slotId = slotId;
            $scope.date = date;
            $("#deletemodal").modal('show');
        }
        $scope.cancel = () => {
            $http.delete("http://localhost:9408/api/TherapistAvailability/" + $scope.id).
            then((response) => {
                console.log(response);
                getAll();
            }, (error) => {
                console.log(error);
            });
            if ($scope.isBusy == true) {

                $http.put("http://localhost:9408/api/Sessions/PutSessionByAssignNewTherapist?sessiondate=" + $scope.date + "&therapistId=" + $scope.therapistId + "&slotId=" + $scope.slotId).
                then((response) => {
                    console.log(response);
                    console.log("updatesession")
                }, (error) => {
                    console.log(error);
                });
            }
            $("#deletemodal").modal("toggle");
        }

        $scope.add = () => {
            $location.path("/createTherapistAvailability");
        }


    }
])