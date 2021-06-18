const { merge } = require('webpack-merge');
const common = require('./webpack.common.js');

module.exports = merge(common, {
  mode: 'development',
  devtool: 'inline-source-map',

  entry: {
    'js/app': './src/app.ts',
    'css/site': './src/scss/site.scss'
  },
    
  output: {
    clean: false,
  },
});
