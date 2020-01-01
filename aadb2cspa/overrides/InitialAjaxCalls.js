Ext.define('InitialAjaxCalls', {
	singleton: true,

	alternateClassName: ['InitialAjaxCalls'],

	config: {
		listFunctions: []
	},

	constructor: function(config) {
		this.initConfig(config);
	},

	addToAjaxCalls: function(delegate) {
		var me = this;
		
		var listFunctions = me.getListFunctions();

		listFunctions.push(delegate);

		me.setListFunctions(listFunctions);
	},

	executeAjaxCalls: function() {
		var me = this;

		Ext.Array.forEach(me.getListFunctions(), function(delegate) {
			delegate();
		});
	}
});