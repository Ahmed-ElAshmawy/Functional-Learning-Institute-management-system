angular.module("module").controller("deleteTherapist", ["$scope", "getAjax", "$http", "$location", "$routeParams", "serverUrl",
    function($scope, getAjax, $http, $location, $routeParams, serverUrl) {
        console.log("Hello From delete Page");
        $scope.Id = $routeParams.Id;
        console.log($scope.Id);
        $('#myModal').modal('show');
        getAjax.response(serverUrl + "api/therapists/" + $scope.Id)
            .then(function(data) {
                $scope.therapists = data.data;
                $scope.UserName = data.data.UserName;
                $scope.StaffName = data.data.StaffName;
                $scope.LastName = data.data.LastName;
                $scope.Id = data.data.Id;
                console.log($scope.therapists);
            }, function(error) {
                console.log(error)
            });
        $scope.confirmDelete = function() {
            console.log("enter delete event of modal");
            var url = serverUrl + "api/therapists/" + $scope.Id;
            var data = $scope.therapistObject;
            var config = 'contenttype';
            $http.delete(url, config).then(function(response) {
                console.log(response);
                if (response.status == 200) {
                    $('#myModal').modal('hide');
                    $('.in').hide();
                    $location.path("/therapist");
                }
            }, function(error) {
                console.log(error);
            });
        }
        $scope.back = function() {
            console.log("enter back event of modal");
            $('#myModal').modal('hide');
            $('.in').hide();
            $location.path("/therapist");
        }
    }
]);