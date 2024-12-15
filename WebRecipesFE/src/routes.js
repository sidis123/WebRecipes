import React from 'react'

const HomePage = React.lazy(() => import('./views/home/HomePage'))
const Colors = React.lazy(() => import('./views/theme/colors/Colors'))
const CreateRecipe = React.lazy(() => import('./views/home/CreateRecipe'))
const EditRecipe = React.lazy(() => import('./views/home/EditRecipe'))

const routes = [
  { path: '/', exact: true, name: 'Home' },
  { path: '/home', name: 'HomePage', element: HomePage },
  { path: '/theme/colors', name: 'Colors', element: Colors },
  { path: '/create-recipe', name: 'CreateRecipe', element: CreateRecipe },
  { path: '/edit-recipe/:id', name: 'EditRecipe', element: EditRecipe },
]

export default routes
