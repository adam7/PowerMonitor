(function () {
    'use strict';
    var controllerId = 'dashboard';
    angular.module('app').controller(controllerId, ['$http', dashboard]);

    function dashboard($http) {
        var vm = this;
        vm.title = 'Dashboard';
        vm.RollingSystemFrequency = { title : "Rolling System Frequency" };
        vm.GenerationByFuelType = { title : "Generation By Fuel Type" };
        vm.GenerationByFuelTypeHistoric = { title: "Generation By Fuel Type Historic" };
        vm.ForecastDemand = { title: "Forecast Demand" };
        vm.getOutputByYear = getOutputByYear;
        vm.OutputByYear = {
            title: "National Output Year Ahead",
            year: 1            
        };
        activate();

        function activate() {
            Chart.defaults.global.responsive = true;
            Chart.defaults.global.animation = false;

            var promises = [
                getRollingSystemFrequency(),
                getGenerationByFuelType(),
                getGenerationByFuelTypeHistoric(),
                getForecastDemand(),
                getOutputByYear()
            ];
        }

        function getGenerationByFuelType() {
            var promise = $http.get('Service/GenerationByFuelType');

            promise.success(function (data) {
                vm.GenerationByFuelType.data = data;
                vm.GenerationByFuelType.options = {};
            }).error(function (error) {
                console.error(error);
            });
        }
        
        function getGenerationByFuelTypeHistoric() {
            var options = {
                datasetFill: false,
                multiTooltipTemplate: "<%= datasetLabel %>: <%= value %>"
            };

            var promise = $http.get('Service/GenerationByFuelTypeHistoric');

            promise.success(function (data) {
                vm.GenerationByFuelTypeHistoric.data = data;
                vm.GenerationByFuelTypeHistoric.options = options;
            }).error(function (error) {
                console.error(error);
            });
        }

        function getRollingSystemFrequency() {
            var options =  {
                pointHitDetectionRadius: 1
            };

            var promise = $http.get('Service/RollingSystemFrequency');

            promise.success(function (data) {
                vm.RollingSystemFrequency.data = data;
                vm.RollingSystemFrequency.options = options;
            }).error(function (error) {
                console.error(error);
            });
        }

        function getForecastDemand() {
            var promise = $http.get('Service/ForecastDemand');

            promise.success(function (data) {
                vm.ForecastDemand.data = data;
                vm.ForecastDemand.options = {};
            }).error(function (error) {
                console.error(error);
            });
        }

        function getOutputByYear() {
            var promise = $http.get("Service/OutputByYear/?year=" + vm.OutputByYear.year);

            promise.success(function (data) {
                vm.OutputByYear.data = data;
                vm.OutputByYear.options = {};
            }).error(function(error){
                console.error(error);
            });
        }
    }
})();