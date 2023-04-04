

module.exports = (sequelize, DataTypes) => {
	return sequelize.define('restaurants', {
		place_id: {
			type: DataTypes.STRING,
			allowNull: false,
			primaryKey: true,
		},
		tags: {
			type: DataTypes.STRING,
			allowNull: false,
		},
		name: {
			type: DataTypes.STRING,
			allowNull: false,
		},
		address: {
			type: DataTypes.STRING,
			allowNull: false,
		},
		working_hours: {
			type: DataTypes.STRING,
			allowNull: false,
		},
		delivery: {
			type: DataTypes.BOOLEAN,
			allowNull: false,
		},
		dine_in: {
			type: DataTypes.BOOLEAN,
			allowNull: false,
		},
		photo_refrences: {
			type: DataTypes.STRING,
			allowNull: false,
		},
		price_level:{
			type: DataTypes.INTEGER,
			allowNull:false,
		}
		rating:{
			type: DataTypes.DECIMAL,
			allowNull:false,
		}
		phone_number: {
			type: DataTypes.STRING,
			allowNull: true,
		},
		website: {
			type: DataTypes.STRING,
			allowNull: true,
		},
		url:{
			type: DataTypes.STRING,
			allowNull: false,
		},
		password: {
			type: DataTypes.STRING,
			allowNull: false,
		},
		confirmedEmail: {
			type: DataTypes.BOOLEAN,
			allowNull: false,
			defaultValue: false,
		},
		
	});
};