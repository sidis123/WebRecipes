import React, { useEffect } from 'react'
import { CContainer } from '@coreui/react'
import { useParams } from 'react-router-dom'
import CreateUser from './CreateUser'
const EditUser = () => {
  const { id } = useParams()

  useEffect(() => {
    console.log('id:', id)
  }, [])
  return (
    <div>
      <CContainer className="px-4 mt-4" lg>
        <CreateUser userioId={id} />
      </CContainer>
    </div>
  )
}
export default EditUser
