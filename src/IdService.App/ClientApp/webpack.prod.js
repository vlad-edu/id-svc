const { merge } = require('webpack-merge');
const common = require('./webpack.common.js');

const TerserPlugin = require("terser-webpack-plugin");
const CssMinimizerPlugin = require('css-minimizer-webpack-plugin');

module.exports = merge(common, {
  mode: 'production',
  devtool: 'source-map',

  entry: {
    'js/app.min': './src/app.ts',
    'css/site.min': './src/scss/site.scss'
  },

  output: {
    clean: true,
  },

  optimization: {
    minimize: true,
    minimizer: [
      new TerserPlugin({
        extractComments: false,
        terserOptions: {
          toplevel: true,
          output: {
            comments: false,
          },
        },
      }),
      new CssMinimizerPlugin({
       minimizerOptions: {
          preset: [
            'default',
            {
              discardComments: { removeAll: true },
            },
          ],
        },
        minify: CssMinimizerPlugin.cssnanoMinify,
        parallel: true,
      }),
    ],
  },
});
