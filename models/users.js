

module.exports = (sequelize, DataTypes) => {
	return sequelize.define('users', {
		username: {
			type: DataTypes.STRING,
			allowNull: false,
			unique: true,
		},
		email: {
			type: DataTypes.STRING,
			allowNull: false,
			unique: true,
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