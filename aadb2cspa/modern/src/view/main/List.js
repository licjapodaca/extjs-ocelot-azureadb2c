/**
 * This view is an example list of people.
 */
Ext.define('aadb2cspa.view.main.List', {
    extend: 'Ext.tab.Panel',
    xtype: 'app-main',

    requires: [
        'Ext.MessageBox',
        'Ext.layout.Fit',
		'aadb2cspa.store.Personnel'
    ],

    defaults: {
        tab: {
            iconAlign: 'top'
        }
	},

    tabBarPosition: 'bottom',

    items: [
        // TODO - Replace the content of this view to suit the needs of your application.
        {
            title: 'Home',
            iconCls: 'x-fa fa-home',
            layout: 'fit',
            // The following grid shares a store with the classic version's grid as well!
            items: [{
				// extend: 'Ext.grid.Grid',
				xtype: 'grid',
			
				// requires: [
				// ],
			
				// title: 'Personnel',
			
				store: {
					type: 'personnel'
				},
			
				columns: [{ 
					text: 'Name',
					dataIndex: 'name',
					width: 100,
					cell: {
						userCls: 'bold'
					}
				}, {
					text: 'Email',
					dataIndex: 'email',
					width: 230 
				}, { 
					text: 'Phone',
					dataIndex: 'phone',
					width: 150 
				}],
			
				listeners: {
					select: 'onItemSelected'
				}
			}]
        },{
            title: 'Users',
            iconCls: 'x-fa fa-user',
            bind: {
                html: '{claims}'
            }
        },{
            title: 'Groups',
            iconCls: 'x-fa fa-users',
            bind: {
                html: '{loremIpsum}'
            }
        },{
            title: 'Settings',
            iconCls: 'x-fa fa-cog',
            bind: {
                html: '{datos}'
            }
        }
    ]
});
