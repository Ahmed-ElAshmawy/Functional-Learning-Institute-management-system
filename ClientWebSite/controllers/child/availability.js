angular.module("module").controller("childAvailability", ["$scope", "getAjax", "$filter", "$http", "$location", "$routeParams",
    function($scope, getAjax, $filter, $http, $location, $routeParams) {
        $scope.childId = $routeParams.id;
        console.log($scope.childId)
        $scope.isLoading = true;
        getAll = () => {
            getAjax.response("http://localhost:9408/api/ChildrenAvailability/GetAvailabilityForChildById?id=" + $scope.childId).
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
            getAjax.delete("http://localhost:9408/api/ChildrenAvailability/" + $scope.id).
            then((response) => {
                console.log(response);
                getAll();
            }, (error) => {
                console.log(error);
            });
            if ($scope.isBusy == true) {
                getAjax.delete("http://localhost:9408/api/Sessions/DeleteSessionByDateAndSlot?date=" + $scope.date + "&childId=" + $scope.childId + "&slotId=" + $scope.slotId).
                then((response) => {
                    console.log(response);
                    getAll();
                }, (error) => {
                    console.log(error);
                });
            }
            $("#deletemodal").modal("toggle");


        }
        $scope.add = () => {
            $location.path("/createChildAvailability/" + $scope.childId);
        }


    }
])