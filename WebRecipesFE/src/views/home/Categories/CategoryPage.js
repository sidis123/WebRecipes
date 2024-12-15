import React, { useState } from 'react'
import { CContainer } from '@coreui/react'
import AppSidebar from '../../../components/AppSidebar'
import CreateCategory from './CreateCategory'
import AllCategories from './AllCategories'

const CategoryPage = () => {
  const [needRefetch, setNeedRefetch] = useState(false)
  const refetch = () => {
    setNeedRefetch(!needRefetch)
  }
  return (
    <>
      <AppSidebar />
      <div className="body flex-grow-1">
        <CContainer className="px-4 mt-4" lg>
          <CreateCategory needRefetch={refetch} />
          <AllCategories needRefetch={needRefetch} />
        </CContainer>
      </div>
    </>
  )
}
export default CategoryPage
