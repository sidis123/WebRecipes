import React from 'react'

const HomePage = React.lazy(() => import('./views/home/HomePage'))
const Colors = React.lazy(() => import('./views/theme/colors/Colors'))
const CreateRecipe = React.lazy(() => import('./views/home/CreateRecipe'))
const EditRecipe = React.lazy(() => import('./views/home/EditRecipe'))
const CategoryPage = React.lazy(() => import('./views/home/Categories/CategoryPage'))
const EditCategory = React.lazy(() => import('./views/home/Categories/EditCategory'))
const UserPage = React.lazy(() => import('./views/home/Useriai/UsersPage'))
const EditUser = React.lazy(() => import('./views/home/Useriai/EditUser'))
const routes = [
  { path: '/', exact: true, name: 'Home' },
  { path: '/home', name: 'HomePage', element: HomePage },
  { path: '/theme/colors', name: 'Colors', element: Colors },
  { path: '/create-recipe', name: 'CreateRecipe', element: CreateRecipe },
  { path: '/edit-recipe/:id', name: 'EditRecipe', element: EditRecipe },
  { path: '/category', name: 'CategoryPage', element: CategoryPage },
  { path: '/edit-category/:id', name: 'EditCategory', element: EditCategory },
  { path: '/user', name: 'UserPage', element: UserPage },
  { path: '/edit-user/:id', name: 'EditUser', element: EditUser },
]

export default routes
