angular.module("module").controller("test", ["$scope", "getAjax", "$http", "$location", "postAjax", "auth", "serverUrl",
    function($scope, getAjax, $http, $location, postAjax, auth, serverUrl) {
        $scope.showdate = true;
        $scope.table = false;
        $scope.showslots = false;
        $scope.thnumbers = false;

        var child_id = $location.search().Id;
        $scope.child_name = $location.search().Name;
        console.log($scope.child_name)
        $scope.data = [];
        $("#date").change((x) => {
            var date = $("#date").val();
            if (new Date(new Date().toLocaleDateString()) <= new Date($("#date").val())) {
                $scope.showdate = false;
                $scope.table = true;
            } else {
                $scope.showdate = true;
                $scope.table = false;
            }
            $scope.calc(date);
            $scope.get(date);
        })
        $('body').on('click', 'input',
            function() {

                $scope.checkedSlots = [];
                $scope.selectedarray = []
                $("[data-checkbox]").each((val, inn) => {
                    if ($(inn).is(":checked")) {
                        var selected = $(inn).attr("data-id");
                        $scope.checkedSlots.push($scope.slotIds[selected]);
                        $scope.selectedarray.push(selected);
                        $scope.check($scope.selectedarray);
                    }

                })
                if ($scope.checkedSlots.length > 0) {
                    $("#showchecked").show();
                    getAjax.response(serverUrl + "api/TherapistAvailability/GetBySlot?date=" + $("#date").val() + "&" + $.param({ slots: $scope.checkedSlots }))
                        .then((data) => {
                            console.log("data", data.data);
                            $scope.obj = data.data;
                            if ($scope.obj.length == 0)
                                $scope.thnumbers = true;
                            else
                                $scope.thnumbers = false;

                        }, (error) => {

                        });
                } else {
                    $("#showchecked").hide();
                }
            });
        $scope.get = (date) => {
            getAjax.response(serverUrl + "api/ChildrenAvailability/GetDateAndId?date=" + date + "&childId=" + child_id + "&isBusy=false").
            then((data) => {
                var temp = [];
                for (var j = 0; j < $scope.slotIds.length; j++) {
                    var res = data.data.slots.indexOf($scope.slotIds[j]);
                    if (res == -1) {
                        temp.push(false);
                    } else
                        temp.push(true);
                }
                $scope.childSlots = temp;
            }, (error) => {

            });
        }
        $scope.calc = (date) => {
            getAjax.response(serverUrl + "api/slots").
            then(function(data) {
                var slots = [];
                var from = [];
                for (var index = 0; index < 48; index++) {
                    slots.push(data.data[index].id);
                    from.push(data.data[index]);
                }
                $scope.content = from;
                $scope.slotIds = slots;
                getAjax.response(serverUrl + "api/test/getby?date=" + date).
                then((response) => {
                    var ar = [];
                    $scope.ids = response.data;
                    for (var i = 0; i < response.data.length; i++) {
                        $scope.id = response.data[i].id;
                        var slotitem = response.data[i].slots;
                        var temp = [];
                        temp.push(response.data[i].name)
                        for (var j = 0; j < slots.length; j++) {
                            var res = slotitem.indexOf(slots[j]);
                            if (res == -1) {
                                temp.push(false);
                            } else
                                temp.push(true);
                        }
                        ar.push(temp);
                    }
                    $scope.slot = ar;
                }, (error) => { console.log(error.statusText) });


            }, function(error) {
                console.log("error");
            });
        }
        $scope.create = () => {
            var Slots = []
            $scope.checkedSlots.forEach(function(element) {
                Slots.push({ Id: element })
            }, this);

            var obj = {
                TherapistId: $("#thnames").val(),
                Date: $("#date").val(),
                Children: [{ Id: child_id }],
                Slots: Slots,
                serviceTypeId: $("#serviceTypeId").val()
            }
            console.log(obj)
            $http.post(serverUrl + "api/Sessions", obj, {
                headers: {
                    'Authorization': auth.getToken()
                }
            }).then((response) => { console.log(response), $location.path("/session") }, (error) => { console.log(error.statusText); })
        }
        $scope.check = (array) => {
            if (array.length > 1) {
                for (var i = 0; i < array.length - 1; i++) {
                    if (Math.abs(array[i] - array[i + 1]) > 1)
                        $scope.showslots = true
                    else
                        $scope.showslots = false
                }
            } else
                $scope.showslots = false

        }
    }
]);