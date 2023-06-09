'use client'
import { useRouter } from 'next/navigation'
import { Input } from '@/components/Input'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { z } from 'zod'
import { AxiosError } from 'axios'
import { api } from '@/lib/api'
import Link from 'next/link'
import { ArrowLeft } from 'lucide-react'

const registerFormSchema = z.object({
  nome: z.string(),
  email: z.string().email({ message: 'Digite um e-mail valído!' }),
  telefone: z
    .string()
    .min(9, { message: 'O telefone precisa de no minimo 9 digitos' }),
})

type RegisterFormData = z.infer<typeof registerFormSchema>

export default function New() {
  const router = useRouter()
  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<RegisterFormData>({
    resolver: zodResolver(registerFormSchema),
  })

  async function handleRegister(data: RegisterFormData) {
    try {
      await api.post('/pessoas', {
        nome: data.nome,
        email: data.email,
        telefone: data.telefone,
      })

      router.push('/')
    } catch (err) {
      if (err instanceof AxiosError && err.response?.data.message) {
        alert(err.response.data.message)
      }
      console.log(err)
    }
  }

  return (
    <div className="m-auto mt-[144px] max-w-lg">
      <div className="mb-[36px] flex items-center justify-between">
        <h1 className="text-3xl">Criar usuário</h1>
        <div className="flex items-center gap-2 hover:text-gray-200">
          <ArrowLeft size={18} />
          <Link href="/">voltar</Link>
        </div>
      </div>

      <form
        onSubmit={handleSubmit(handleRegister)}
        className="flex flex-col gap-4"
      >
        <div>
          <Input {...register('nome')} placeholder="Nome:" />
          {errors.nome && <p>{errors.nome.message}</p>}
        </div>
        <div>
          <Input {...register('email')} placeholder="E-mail:" />
          {errors.email && <p>{errors.email.message}</p>}
        </div>
        <div>
          <Input {...register('telefone')} placeholder="Telefone:" />
          {errors.telefone && <p>{errors.telefone.message}</p>}
        </div>

        <button
          className="h-[56px] rounded-[10px] bg-[#FF9000] text-[#312E38] hover:bg-[#d88d2b] disabled:cursor-not-allowed"
          type="submit"
          disabled={isSubmitting}
        >
          Salvar
        </button>
      </form>
    </div>
  )
}
