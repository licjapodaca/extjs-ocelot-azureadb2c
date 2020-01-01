/**
 * This view is an example list of people.
 */
Ext.define('aadb2cspa.view.main.List', {
    extend: 'Ext.grid.Panel',
    xtype: 'mainlist',

    requires: [
        'aadb2cspa.store.Personnel'
    ],

    title: 'Personnel',

    store: {
        type: 'personnel'
    },

    columns: [
        { text: 'Name',  dataIndex: 'name' },
        { text: 'Email', dataIndex: 'email', flex: 1 },
        { text: 'Phone', dataIndex: 'phone', flex: 1 }
	],
	
	dockedItems: [{
		xtype: 'toolbar',
		dock: 'top',
		items: [
			{
				xtype: 'button',
				text: 'Logout',
				handler: function() {
					window.msalInstance.logout();
				}
			}
		]
	}],

    listeners: {
        select: 'onItemSelected'
    }
});
