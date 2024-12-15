import React from 'react'
import { CContainer } from '@coreui/react'
import AppSidebar from '../../components/AppSidebar'
import { useParams } from 'react-router-dom'
import CreateRecipe from './CreateRecipe'
const EditRecipe = () => {
  const { id } = useParams()

  return (
    <div>
      <AppSidebar />

      <CContainer className="px-4 mt-4" lg>
        <CreateRecipe recipeId={id} />
      </CContainer>
    </div>
  )
}
export default EditRecipe
