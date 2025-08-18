import { useQuery, useQueryClient } from "@tanstack/react-query";
import { useAuth } from "../context/AuthContext";
import axios from "axios";
import { Tabs, Flex, Title, Stack, Button, Text, Box, Card, TabsList } from "@mantine/core";
import { useState } from "react";


interface Zaposleni {
  id: number,
  ime: string,
  prezime: string
}

interface Comment {
  id: number,
  komentar: string
}

export default function Admin() {
  const { user, setUser } = useAuth();
  const queryClient = useQueryClient();
  const [activeTab, setActiveTab] = useState<string | null>("komentari");

  const { isLoading: areZaposleniLoading, data: users, isError: usersError } = useQuery<Zaposleni[]>({
    queryKey: ["zaposleni_list", user?.id],
    queryFn: async () => {
      return await axios
        .get(`https://localhost:7080/zaposleni/${user?.id}`)
        .then((resp) => {
          return resp.data;
        })
        .catch((err) => {
          console.log(err);
          return [];
        });
    },
    enabled: !!user?.id,
  });

  const { isLoading: areCommentsLoading, data: comments, isError: commentsError } = useQuery<Comment[]>({
    queryKey: ["all_comments"],
    queryFn: async () => {
      return await axios
        .get('https://localhost:7080/komentari')
        .then((resp) => {
          return resp.data;
        })
        .catch((err) => {
          console.log(err);
          return [];
        });
    },
  });

  const deleteZaposleni = async (zaposeniId: number, adminId: number) => {
    try {
      await axios.delete(`https://localhost:7080/Admin/zaposleniDelete/${adminId}/${zaposeniId}`);
      queryClient.invalidateQueries({ queryKey: ["zaposleni_list", user?.id] });
    } catch (err: any) {
      console.error(err);
      alert(err.response.data);
    }
  }



  const deleteComment = async (commentId: number, adminId: number) => {
    try {
      await axios.delete(`https://localhost:7080/Admin/brisiKomentar/${adminId}/${commentId}`);
      queryClient.invalidateQueries({ queryKey: ["all_comments"] });
    } catch (err: any) {
      console.error(err);
      alert(err.response.data);
    }
  }
  console.log()
  return (
    <Card shadow="sm" p="lg" radius="md" withBorder>
      <Tabs value={activeTab} onChange={setActiveTab}>
        <Tabs.List>
          <Tabs.Tab value="komentari">Komentari</Tabs.Tab>
          <Tabs.Tab value="zaposleni">Zaposleni</Tabs.Tab>
        </Tabs.List>

        <Tabs.Panel value="komentari" pt="xs">
          <Title order={3} mb="md">Komentari</Title>
          {areCommentsLoading && <Text>Ucitavanje komentara</Text>}
          {commentsError && <Text>Greska pri ucitavanju komentara</Text>}
          <Stack>
            {comments?.map((kom) => (
              <Flex
                key={kom.id}
                justify="space-between"
                align="center"
                p="sm"
                style={{ border: "1px solid #120d0dff", borderRadius: "8px" }}
              >
                <Text >{kom.komentar}</Text>

                <Button color="red" size="xs" onClick={() => deleteComment(kom.id, user?.id)}>
                  Obrisi
                </Button>
              </Flex>
            ))}
          </Stack>
        </Tabs.Panel>

        <Tabs.Panel value="zaposleni" pt="xs">
          <Title order={3} mb="md">Zaposleni</Title>
          {areZaposleniLoading && <Text>Ucitavanje radnika...</Text>}
          {usersError && <Text>Greska pri ucitavanju radnika</Text>}
          {users?.map((zap) => (
            <Flex
              key={zap.id}
              align="center"
              justify="space-between"
              p="sm"
              style={{ border: "1px solid #ddd", borderRadius: "8px" }}
            >
              <Text>{zap.ime} {zap.prezime}</Text>
              <Button color="red" size="xs" onClick={() => deleteZaposleni(zap.id, user?.id)}>Obrisi</Button>
            </Flex>
          ))}
        </Tabs.Panel>
      </Tabs>
    </Card>
  );
}