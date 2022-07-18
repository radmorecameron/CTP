def get_even_list(num_list):
    even_list = []
    for el in num_list:
        if el % 2 == 0:
            even_list.append(el)
    return even_list


def get_even_count(num_list):
    return len(get_even_list(num_list))
