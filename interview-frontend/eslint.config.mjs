import pluginReact from 'eslint-plugin-react';
import reactHooks from 'eslint-plugin-react-hooks';
import jsxA11y from 'eslint-plugin-jsx-a11y';

export default [
  {
    files: ['**/*.{js,jsx}'],
    ignores: ['node_modules', 'dist', 'build'],
    languageOptions: {
      ecmaVersion: 2022,
      sourceType: 'module',
      parserOptions: {
        ecmaFeatures: {
          jsx: true
        },
      },
    },
    plugins: {
      react: pluginReact,
      'react-hooks': reactHooks,
      'jsx-a11y': jsxA11y
    },
    rules: {
      ...pluginReact.configs.recommended.rules,
      'react/react-in-jsx-scope': 'off',
      'react-hooks/rules-of-hooks': 'error',
      'react-hooks/exhaustive-deps': 'warn',
      ...jsxA11y.configs.recommended.rules,
      'no-unused-vars': 'warn',
      'prefer-const': 'error',
      'semi': ['error', 'always'],
      'react/prop-types': 'off'
    },
    settings: {
      react: {
        version: 'detect',
      },
    },
  },
];