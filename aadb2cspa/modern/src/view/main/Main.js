/**
 * This class is the main view for the application. It is specified in app.js as the
 * "mainView" property. That setting causes an instance of this class to be created and
 * added to the Viewport container.
 */
Ext.define('aadb2cspa.view.main.Main', {
	extend: 'Ext.Panel',

	xtype: 'app-main',

	controller: 'main',
	viewModel: 'main',

	layout: 'card',

	bind: {
		title: '{name}'
	},

	tools: [{
		iconCls: 'x-fa fa-sign-out-alt',
		handler: function () {
			window.msalInstance.logout();
		}
	}],

	items: [{
		xtype: 'mainlist'
	}],

	listeners: {
		beforeshow: 'initialize'
	}
});
