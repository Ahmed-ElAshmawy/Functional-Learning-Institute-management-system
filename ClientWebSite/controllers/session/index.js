angular.module("module").controller("session", ["$scope", "getAjax", "$http", "$location", "serverUrl",
    function($scope, getAjax, $http, $location, serverUrl) {
        getAjax.get(serverUrl + "api/Sessions/GetByRecentDate").
        then(function(data) {
            $scope.show = data.data
        }, function(error) {
            if (error.status == "401")
                $location.path("/unauthorized");
            console.log(error);
        });
        getAjax.get(serverUrl + "api/Children").
        then(function(data) {
            console.log(data.data);
            $scope.child = data.data;
        }, function(error) {
            console.log(error);
            if (error.status == "401")
                $location.path("/unauthorized");
        });
        $scope.create = () => {
            $location.path("/newSession/").search({
                Id: $("#childdrop").val(),
                Name: $("#childdrop :selected").text()
            });
        }
        $scope.Showdetails = (id) => {
            $location.path("/sessiondetails/" + id);
        }
        $scope.Showupdate = (id) => {
            var s = $scope.show.filter(item => {
                return item.id == id;
            });

            if (s[0].status == 3)
                $location.path("/updatesession/" + id);
            else
                $location.path("/updatesession/" + id + "/true");
        }
        $scope.sessionFinshing = () => {
            $location.path("/sessionFinshing")
        }
        $scope.group = (id) => {
            $location.path("/assigntogroup/" + id);
        }
    }
]);