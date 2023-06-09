// eslint-disable-next-line no-unused-expressions, prettier/prettier
"use client"
import Image from 'next/image'
import logo from '../assets/logo.png'
import { useEffect, useState } from 'react'
import { api } from '@/lib/api'
import { Cards } from '@/components/Cards'
import Link from 'next/link'
import { useRouter } from 'next/navigation'
import { Users } from 'lucide-react'
import { NextSeo } from 'next-seo'

interface userProps {
  id: string
  nome: string
  email: string
  telefone: string
}

export default function Home() {
  const [users, setUsers] = useState<userProps[]>([])

  const router = useRouter()

  function handleDetails(id: string) {
    router.push(`/details/${id}`)
  }

  useEffect(() => {
    async function fetchUsersApi() {
      const response = await api.get('/pessoas')
      setUsers(response.data)
    }

    fetchUsersApi()
  }, [])

  return (
    <>
      <NextSeo
        title="Home | Tech One"
        description="Veja os usuários cadastrados na pagina, ou faça seu primeiro cadastro!"
      />
      <div className="grid grid-cols-cols2 ">
        <div className="flex h-screen max-h-full max-w-[250px] flex-col items-center justify-between bg-[#232129]">
          <div className="mt-[39px] flex items-center justify-center">
            <Image src={logo} alt="Logo tech one" width={100} />
          </div>
          <div className="flex h-[80px] w-full items-center justify-center bg-[#FF9000]">
            <Link href="/new" className="text-xl text-[#232129]">
              + Criar usuário
            </Link>
          </div>
        </div>
        <div className="w-full p-[64px]">
          {users?.length > 0 ? (
            <h1 className="mb-4 text-3xl">Usuários cadastrados</h1>
          ) : (
            <></>
          )}

          {users?.length > 0 ? (
            users?.map((user) => {
              return (
                <Cards
                  key={user.id}
                  name={user.nome}
                  details={() => {
                    handleDetails(user.id)
                  }}
                />
              )
            })
          ) : (
            <div className="flex h-full flex-col items-center justify-center">
              <h1 className="text-2xl text-gray-200">
                Há não, ainda não tem nenhum usuário cadastrado!
              </h1>
              <p className="text-xl text-gray-200">
                Fique a vontade para cadastrar o primeiro usúario.
              </p>
              <Users size={25} className="text-gray-200" />
            </div>
          )}
        </div>
      </div>
    </>
  )
}
