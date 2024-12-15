import React from 'react'
import CIcon from '@coreui/icons-react'
import { cilSpeedometer, cilApple, cilApps, cilBlind } from '@coreui/icons'
import { CNavItem, CNavTitle } from '@coreui/react'

const _nav = [
  {
    component: CNavItem,
    name: 'Home',
    to: '/home',
    icon: <CIcon icon={cilSpeedometer} customClassName="nav-icon" />,
    allowedRoles: [1, 2, 3], // All roles can see this
  },
  {
    component: CNavTitle,
    name: 'Recipes',
    allowedRoles: [2, 3], // User and Super Admin can see this
  },
  {
    component: CNavItem,
    name: 'Create Recipe',
    to: '/create-recipe',
    icon: <CIcon icon={cilApple} customClassName="nav-icon" />,
    allowedRoles: [2, 3], // Only Super Admin can see this
  },
  {
    component: CNavTitle,
    name: 'Categories',
    allowedRoles: [3], // Only Super Admin
  },
  {
    component: CNavItem,
    name: 'Categories Page',
    to: '/category',
    icon: <CIcon icon={cilApps} customClassName="nav-icon" />,
    allowedRoles: [3], // Only Super Admin
  },
  {
    component: CNavTitle,
    name: 'Users',
    allowedRoles: [3], // Guest and Super Admin
  },
  {
    component: CNavItem,
    name: 'Users Page',
    to: '/user',
    icon: <CIcon icon={cilBlind} customClassName="nav-icon" />,
    allowedRoles: [3], // Guest and Super Admin
  },
]

export default _nav
