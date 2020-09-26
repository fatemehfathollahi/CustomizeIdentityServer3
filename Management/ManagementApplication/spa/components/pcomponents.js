define(['ko'], function (ko) {
	ko.components.register("manager", { require: 'engine/infrastructure/manager/manager.min' });
    ko.components.register("headercomponent", { require: 'components/home/headerComponent.min' });
    ko.components.register("brandingcomponent", { require: 'components/home/brandingComponent.min' });
    ko.components.register("controlscomponent", { require: 'components/home/controlsComponent.min' });
    ko.components.register("homecomponent", { require: 'components/home/homeComponent.min' });
    ko.components.register("menuitemscomponent", { require: 'components/home/menuItemsComponent.min' });
    ko.components.register("confirmcomponent", { require: 'components/common/confirmDialogComponent.min' });
    ko.components.register("mealIndexcomponent", { require: 'components/meal/IndexComponent.min' });
    ko.components.register("mealcomponent", { require: 'components/meal/mealComponent.min' });
    ko.components.register("meallistcomponent", { require: 'components/meal/mealListComponent.min' });
    ko.components.register("restaurantIndexcomponent", { require: 'components/restaurant/indexComponent.min' });
    ko.components.register("restaurantcomponent", { require: 'components/restaurant/restaurantComponent.min' });
    ko.components.register("foodIndexcomponent", { require: 'components/food/indexComponent.min' });
    ko.components.register("foodcomponent", { require: 'components/food/foodComponent.min' });
    ko.components.register("restaurantfoodpackageIndexcomponent", { require: 'components/restaurantFoodPackage/indexComponent.min' });
    ko.components.register("restaurantFoodPackageComponent", { require: 'components/restaurantFoodPackage/restaurantFoodPackageComponent.min' });
    ko.components.register("foodlistcomponent", { require: 'components/food/foodListComponent.min' });
    ko.components.register("copyFoodPackageComponent", { require: 'components/restaurantFoodPackage/copyFoodPackageComponent.min' });
    ko.components.register("foodcrudcomponent", { require: 'components/foodTypes/FoodTypesComponent.min' });
    ko.components.register("foodTypeIndexcomponent", { require: 'components/foodTypes/IndexComponent.min' });
    ko.components.register("personelIndexcomponent", { require: 'components/personel/IndexComponent.min' });
    ko.components.register("personelcomponent", { require: 'components/personel/personelComponent.min' });
    ko.components.register("restaurantlistcomponent", { require: 'components/restaurant/restaurantListComponent.min' });
    ko.components.register("rimsindexcomponent", { require: 'components/rimsSetting/indexComponent.min' });
    ko.components.register("prebuyindexcomponent", { require: 'components/preBuy/indexComponent.min' });
    ko.components.register("personellistcomponent", { require: 'components/personel/personelListComponent.min' });
    ko.components.register("prebuycomponent", { require: 'components/preBuy/prebuyComponent.min' });
    ko.components.register("foodprebuycomponent", { require: 'components/food/foodPrebuyComponent.min' });
    ko.components.register("importpersonellistcomponent", { require: 'components/personel/importPersonelListComponent.min' });
    ko.components.register("deviceindexcomponent", { require: 'components/device/indexComponent.min' });
    ko.components.register("devicecomponent", { require: 'components/device/deviceComponent.min' });
    ko.components.register("auditindexcomponent", { require: 'components/auditEvent/indexComponent.min' });
    ko.components.register("auditcomponent", { require: 'components/auditEvent/auditComponent.min' });

    ko.applyBindings({});
})