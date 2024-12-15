import React from 'react'
import { AppContent, AppSidebar, AppFooter, AppHeader } from '../components/index'

const DefaultLayout = () => {
  return (
    <div className="d-flex flex-column min-vh-100">
      <AppSidebar />
      <div className="wrapper flex-grow-1 d-flex flex-column">
        <AppHeader />
        <div className="body flex-grow-1 px-4">
          <AppContent />
        </div>
        <AppFooter />
      </div>
    </div>
  )
}

export default DefaultLayout
