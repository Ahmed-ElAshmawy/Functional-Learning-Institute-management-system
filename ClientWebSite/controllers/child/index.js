angular.module("module").controller("child", ["$scope", "getAjax", "$http", "$location", "serverUrl",
    function($scope, getAjax, $http, $location, serverUrl) {
        console.log("Hello From child");
        var id = $location.search().Id;
        $scope.isLoading = true;

        function getdata() {
            getAjax.response(serverUrl + "api/Parents/GetParentChild?id=" + id).
            then((response) => {
                $scope.isLoading = false;
                for (var i = 0; i < response.data.length; i++) {
                    switch (response.data[i].fundGroup) {
                        case 0:
                            response.data[i].fundGroup = "Under 6"
                            break;
                        case 1:
                            response.data[i].fundGroup = "From 6 to 18"
                            break;
                        case 2:
                            response.data[i].fundGroup = "Over 18"
                            break;
                        case 3:
                            response.data[i].fundGroup = "Home Funding"
                            break;
                        case 4:
                            response.data[i].fundGroup = "Out of Pocket"
                            break;
                        case 5:
                            response.data[i].fundGroup = "AFU"
                            break;
                    }
                }
                $scope.data = response.data;
            }, (error) => {
                console.log(error.statusText)
            });
        }
        getdata();
        $scope.delete = function(id) {
            console.log(id)
            $scope.deleteId = id;
            $('#cmodal').modal('show');          
        }
        $scope.confirmDelete = function() {
            $('#cmodal').modal("toggle");
            $http.delete(serverUrl + "api/Children/" + $scope.deleteId)
                .then((response) => {
                        console.log(response.statusText);
                        $scope.data = $scope.data.filter(item => {
                            return item.id != $scope.deleteId;
                        });
                        $scope.deleteId = "";
                    },
                    (error) => {
                        $('#errorModal').modal("toggle");
                        console.log(error.statusText)
                    });
        }
        $scope.create = () => {
            $location.path("/createchild/").search({
                Id: id
            });
        }
        $scope.update = (cid) => {
            $location.path("/updatechild/").search({
                Id: id,
                cId: cid
            });
        }
        $scope.availability = (id) => {
            $location.search({});
            $location.path("/childAvailability/" + id);
        }
    }
]);