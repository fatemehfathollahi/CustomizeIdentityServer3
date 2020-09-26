define(['ko'], function (ko) {
	ko.components.register("manager", { require: 'engine/infrastructure/manager/manager' });
    ko.components.register("headercomponent", { require: 'components/home/headerComponent' });
    ko.components.register("brandingcomponent", { require: 'components/home/brandingComponent' });
    ko.components.register("controlscomponent", { require: 'components/home/controlsComponent' });
    ko.components.register("homecomponent", { require: 'components/home/homeComponent' });
    ko.components.register("menuitemscomponent", { require: 'components/home/menuItemsComponent' });
    ko.components.register("confirmcomponent", { require: 'components/common/confirmDialogComponent' });
    ko.components.register("clinetindexcomponent", { require: 'components/client/indexComponent' });
    ko.components.register("clinetcomponent", { require: 'components/client/clientComponent' });
    ko.components.register("corsorigincomponent", { require: 'components/corsOrigin/corsOriginComponent' });
    ko.components.register("postlogoutcomponent", { require: 'components/postLogoutRedirectURI/postLogoutComponent' });
    ko.components.register("clientredirectcomponent", { require: 'components/clientRedirect/clientRedirectComponent' });
    ko.components.register("clientscopscomponent", { require: 'components/clientScop/clientScopsComponent' });
    ko.components.register("clientsecretscomponent", { require: 'components/clientSecret/clientSecretComponent' });
    ko.components.register("scopeindexcomponent", { require: 'components/scope/indexComponent' });
    ko.components.register("scopecomponent", { require: 'components/scope/scopeComponent' });
    ko.components.register("scopeclaimcomponent", { require: 'components/scopeClaim/scopeClaimComponent' });
    ko.components.register("scopesecretcomponent", { require: 'components/scopeSecret/scopeSecretComponent' });
    ko.components.register("roleindexcomponent", { require: 'components/role/indexComponent' });
    ko.components.register("rolecomponent", { require: 'components/role/roleComponent' });
    ko.components.register("userindexcomponent", { require: 'components/user/indexComponent' });
    ko.components.register("usercomponent", { require: 'components/user/userComponent' });
    ko.components.register("permisioncomponent", { require: 'components/permision/permisionComponent' });
    ko.components.register("permissionindexcomponent", { require: 'components/permision/indexComponent' });
    ko.components.register("permissionlistcomponent", { require: 'components/permision/permisionListComponent' });
    ko.components.register("clientpermissionlistcomponent", { require: 'components/userClientPermission/clientPermisionListComponent' });

    ko.applyBindings({});
})