angular.module("module").controller("therapist", ["$scope", "$http", "$location", "getAjax", "serverUrl",
    function($scope, $http, $location, getAjax, serverUrl) {
        console.log("Enter therapist Page");
        $scope.isLoading = true;

        getAjax.response(serverUrl + "api/therapists")
            .then(function(data) {
                $scope.therapists = data.data;
                $scope.isLoading = false;
                console.log($scope.therapists);
            }, function(error) {
                console.log(error)
            });
        $scope.editTherapist = (i) => {
            console.log("enter edit click event");
            console.log("id", i);
            $location.path("/updateTherapist/" + i);
            console.log("pass location path");
        }
        $scope.detailsTherapist = (i) => {
            console.log("enter details click event");
            console.log("Id", i);
            $location.path("/therapistDetails/" + i);
        }
        $scope.deleteTherapist = (i, x) => {
            console.log("enter delete click event");
            $scope.deleteId = i;
            $scope.deleteName = x;
            console.log("delete id", $scope.deleteId, "delete name", $scope.deleteName);
            $('#myModal').modal('show');
        }
        $scope.task = (therapistId, therapistName) => {
            $location.path("/therapistTask/").search({
                therapistId: therapistId
            });
        }
        $scope.confirmDelete = function() {
            console.log("enter delete event of modal");
            console.log("delete id", $scope.deleteId, "delete name", $scope.deleteName);
            var url = serverUrl + "api/therapists/" + $scope.deleteId;
            var config = 'contenttype';
            $http.delete(url, config).then(function(response) {
                console.log(response);
                $scope.isLoading = false;
                if (response.status == 200) {
                    getAjax.response(serverUrl + "api/therapists")
                        .then(function(data) {
                            $scope.therapists = data.data;
                            $scope.isLoading = false;
                            console.log($scope.therapists);
                        }, function(error) {
                            console.log(error)
                        });
                    $('#myModal').modal('hide');
                    $('.in').hide();
                }
            }, function(error) {
                console.log(error);
            });

        }
    }
]);