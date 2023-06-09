'use client'
import { useParams, useRouter } from 'next/navigation'
import { Input } from '@/components/Input'

import { AxiosError } from 'axios'
import { api } from '@/lib/api'
import Link from 'next/link'
import { ArrowLeft } from 'lucide-react'
import { useEffect, useState } from 'react'

interface UserProps {
  nome: string
  email: string
  telefone: string
  id: string
}

export default function Details() {
  const router = useRouter()
  const params = useParams()
  const [user, setUser] = useState<UserProps>()
  const [name, setName] = useState('')
  const [email, setEmail] = useState('')
  const [telefone, setTelefone] = useState('')

  async function handleRegister(id: string) {
    try {
      await api.put(`/pessoas/${id}`, {
        nome: name,
        email,
        telefone,
      })

      router.push('/')
    } catch (err) {
      if (err instanceof AxiosError && err.response?.data.message) {
        alert(err.response.data.message)
      }
      console.log(err)
    }
  }

  async function handleDeleteUser(id: string) {
    await api.delete(`/pessoas/${id}`)
    router.push('/')
  }

  useEffect(() => {
    async function fetchUser() {
      const response = await api.get(`/pessoas/${params.id}`)
      setUser(response.data)
      setName(response.data.nome)
      setEmail(response.data.email)
      setTelefone(response.data.telefone)
    }

    fetchUser()
  }, [params.id])

  return (
    <div className="m-auto mt-[144px] max-w-lg">
      <div className="mb-[36px] flex items-center justify-between">
        <h1 className="text-3xl">Atualizar | excluir usuário</h1>
        <div className="flex items-center gap-2 hover:text-gray-200">
          <ArrowLeft size={18} />
          <Link href="/">voltar</Link>
        </div>
      </div>

      <div className="flex flex-col gap-4">
        <div>
          <Input
            placeholder="Nome:"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
        </div>
        <div>
          <Input
            type="email"
            placeholder="E-mail:"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>
        <div>
          <Input
            placeholder="Telefone:"
            value={telefone}
            onChange={(e) => setTelefone(e.target.value)}
          />
        </div>

        <div className="flex w-full items-center justify-between">
          <button
            className="h-[56px] w-[250px] rounded-[10px] bg-[#FF9000] text-[#312E38] hover:bg-[#d88d2b] disabled:cursor-not-allowed"
            onClick={() => handleRegister(user!.id)}
          >
            Atualizar
          </button>

          <button
            className="h-[56px] w-[250px] rounded-[10px] bg-red-600 hover:bg-red-700"
            onClick={() => handleDeleteUser(user!.id)}
          >
            Excluir
          </button>
        </div>
      </div>
    </div>
  )
}
