angular.module("module").controller("assigntogroup", ["$scope", "getAjax", "$http", "$location", "serverUrl", "$routeParams",
    function($scope, getAjax, $http, $location, serverUrl, $routeParams) {
        $scope.sessionId = $routeParams.Id;
        $scope.selectioncheck = true;
        getAjax.get(serverUrl + "api/Children/GetAvailableChildren?id=" + $scope.sessionId).
        then(function(data) {
            $scope.show = data.data
            console.log("dataaaaaaaa", $scope.show)
        }, function(error) {
            if (error.status == "401")
                $location.path("/unauthorized");
            console.log(error);
        });

        $('body').on('click', 'input', function() {
            $scope.selectedchildren = []
            $("[data-checkbox]").each((val, inn) => {
                if ($(inn).is(":checked")) {
                    var selected = $(inn).attr("data-id");
                    //$scope.checkedSlots.push($scope.slotIds[selected]);
                    $scope.selectedchildren.push(selected);
                    console.log("selected", $scope.selectedchildren)
                }

            })

            if ($scope.selectedchildren.length > 0) {
                $scope.selectioncheck = false;
                $scope.$apply()
            } else {
                $scope.selectioncheck = true;
                $scope.$apply()
            }
        })
        $scope.createGroup = () => {
            $http.put("http://localhost:9408/api/Sessions/PutSessionForGroup?id=" + $scope.sessionId + "&" + $.param({ childrenId: $scope.selectedchildren })).
            then((response) => {
                console.log(response)
                $location.path("/session");
            }, (error) => {
                console.log(error);
            });
        }

    }
])