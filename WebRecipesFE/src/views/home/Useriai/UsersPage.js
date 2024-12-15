import React, { useState } from 'react'
import { CContainer } from '@coreui/react'
import AppSidebar from '../../../components/AppSidebar'
import CreateUser from './CreateUser'
import AllUsers from './AllUsers'
const UsersPage = () => {
  const [needRefetch, setNeedRefetch] = useState(false)
  const refetch = () => {
    setNeedRefetch(!needRefetch)
  }
  return (
    <>
      <AppSidebar />
      <div className="body flex-grow-1">
        <CContainer className="px-4 mt-4" lg>
          <CreateUser needRefetch={refetch} />
          <AllUsers needRefetch={needRefetch} />
        </CContainer>
      </div>
    </>
  )
}
export default UsersPage
