import React, { useState } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { useNavigate } from 'react-router-dom'

import {
  CButton,
  CCloseButton,
  CSidebar,
  CSidebarBrand,
  CSidebarFooter,
  CSidebarHeader,
  CSidebarToggler,
} from '@coreui/react'

import { cilLockLocked } from '@coreui/icons'
import CIcon from '@coreui/icons-react'

import { AppSidebarNav } from './AppSidebarNav'

import logo from 'src/assets/brand/logosas.svg'
import { sygnet } from 'src/assets/brand/sygnet'

// sidebar nav config
import navigation from '../_nav'

const AppSidebar = () => {
  const dispatch = useDispatch()
  const unfoldable = useSelector((state) => state.sidebarUnfoldable)
  const sidebarShow = useSelector((state) => state.sidebarShow)
  const navigate = useNavigate()
  const [isHovered, setIsHovered] = useState(false)
  const user = useSelector((state) => state.user) // Access user from Redux

  const handleLogOut = () => {
    localStorage.removeItem('token')
    localStorage.removeItem('userid')
    dispatch({ type: 'reset_state' })
    navigate('/login')
  }
  const buttonStyle = {
    backgroundColor: isHovered ? '#0056b3' : '#007bff',
    color: isHovered ? '#f8f9fa' : 'white',
    transform: isHovered ? 'scale(1.05)' : 'scale(1)',
    boxShadow: isHovered ? '0 4px 8px rgba(0, 0, 0, 0.2)' : 'none',
    transition: 'all 0.3s ease',
    cursor: 'pointer',
    border: 'none',
    padding: '10px 20px',
    borderRadius: '4px',
  }

  return (
    <CSidebar
      className="border-end"
      colorScheme="dark"
      position="fixed"
      unfoldable={unfoldable}
      visible={sidebarShow}
      onVisibleChange={(visible) => {
        dispatch({ type: 'set', sidebarShow: visible })
      }}
    >
      <CSidebarHeader className="border-bottom">
        <h4 style={{ color: '#999' }}>
          <img src={logo} alt="logo" style={{ width: '120%' }} />
        </h4>
        <CSidebarBrand to="/">
          <CIcon customClassName="sidebar-brand-full" icon={logo} height={32} />
          <CIcon customClassName="sidebar-brand-narrow" icon={sygnet} height={32} />
        </CSidebarBrand>
        <CCloseButton
          className="d-lg-none"
          dark
          onClick={() => dispatch({ type: 'set', sidebarShow: false })}
        />
      </CSidebarHeader>
      <AppSidebarNav items={navigation} />
      <CSidebarFooter className="border-top d-none d-lg-flex">
        {user.id_Vartotojas ? (
          <CButton
            onClick={handleLogOut}
            style={buttonStyle}
            onMouseEnter={() => setIsHovered(true)} // Trigger hover state
            onMouseLeave={() => setIsHovered(false)} // Remove hover state
          >
            <CIcon icon={cilLockLocked} className="me-2" />
            Logout
          </CButton>
        ) : (
          <CButton
            onClick={() => navigate('/login')}
            style={buttonStyle}
            onMouseEnter={() => setIsHovered(true)} // Trigger hover state
            onMouseLeave={() => setIsHovered(false)} // Remove hover state
          >
            <CIcon icon={cilLockLocked} className="me-2" />
            Login
          </CButton>
        )}
      </CSidebarFooter>
    </CSidebar>
  )
}

export default React.memo(AppSidebar)
