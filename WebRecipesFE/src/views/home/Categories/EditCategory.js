import React, { useEffect } from 'react'
import { CContainer } from '@coreui/react'
import { useParams } from 'react-router-dom'
import CreateCategory from './CreateCategory'
import CategoryWithRecipes from './CategoryWithRecipes'
const EditCategory = () => {
  const { id } = useParams()

  useEffect(() => {
    console.log('id:', id)
  }, [])
  return (
    <div>
      <CContainer className="px-4 mt-4" lg>
        <CreateCategory categoryId={id} />
        <CategoryWithRecipes category={id} />
      </CContainer>
    </div>
  )
}
export default EditCategory
