(function () {
    'use strict';
    var controllerId = 'dashboard';
    angular.module('app').controller(controllerId, ['common', 'datacontext', '$http', dashboard]);

    function dashboard(common, datacontext, $http) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Dashboard';
        vm.RollingSystemFrequency = { title : "Rolling System Frequency" };
        vm.GenerationByFuelType = { title : "Generation By Fuel Type" };
        vm.GenerationByFuelTypeHistoric = { title: "Generation By Fuel Type Historic" };
        vm.ForecastDemand = { title: "Forecast Demand" };
        activate();

        function activate() {
            var promises = [
                getRollingSystemFrequency(),
                getGenerationByFuelType(),
                getGenerationByFuelTypeHistoric(),
                getForecastDemand()
            ];

            common.activateController(promises, controllerId)
                .then(function () { log('Activated Dashboard View'); });
        }

        function getGenerationByFuelType() {
            var promise = $http.get('Service/GenerationByFuelType');

            promise.success(function (data) {
                vm.GenerationByFuelType.data = data;
                vm.GenerationByFuelType.options = {};
                console.log(vm.GenerationByFuelType.data);
            }).error(function (error) {
                console.error('Error:' + error);
            });
        }
        
        function getGenerationByFuelTypeHistoric() {
            var options = {
                animation: false,
                datasetFill: false,
                multiTooltipTemplate: "<%= datasetLabel %>: <%= value %>"
            };

            var promise = $http.get('Service/GenerationByFuelTypeHistoric');

            promise.success(function (data) {
                vm.GenerationByFuelTypeHistoric.data = data;
                vm.GenerationByFuelTypeHistoric.options = options;
            }).error(function (error) {
                console.error('Error:' + error);
            });
        }

        function getRollingSystemFrequency() {
            var options =  {
                pointHitDetectionRadius: 1,
                animation: false
            };

            var promise = $http.get('Service/RollingSystemFrequency');

            promise.success(function (data) {
                vm.RollingSystemFrequency.data = data;
                vm.RollingSystemFrequency.options = options;
            }).error(function (error) {
                console.error('Error:' + error);
            });
        }

        function getForecastDemand() {
            var promise = $http.get('Service/ForecastDemand');

            promise.success(function (data) {
                vm.ForecastDemand.data = data;
                vm.ForecastDemand.options = {};
            }).error(function (error) {
                console.error('Error:' + error);
            });
        }
    }
})();